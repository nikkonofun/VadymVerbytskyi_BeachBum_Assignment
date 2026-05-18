using System;
using System.Threading;
using Client.Model.GameCommands;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Shared.ClientServerFakeLayer;
using Zenject;

namespace Client.Model.RequestResponseManagement
{
  public class RequestResponseManager
  {
    private FakeNetwork _fakeNetwork;
    private RetryPolicyConfig _retryPolicy;
    
    private IGameCommand _processingCommand;
    private CancellationTokenSource _cancellation;
    private int _retryAttempt;

    public bool RequestInProgress => _processingCommand != null;

    [Inject]
    public void Inject(FakeNetwork fakeNetwork, RetryPolicyConfig retryPolicy)
    {
      _fakeNetwork = fakeNetwork;
      _retryPolicy = retryPolicy;
    }

    public async UniTask TryExecuteCommandAsync(IGameCommand command)
    {
      if (RequestInProgress)
        return;

      CleanData();
      _processingCommand = command;

      try
      {
        var maxAttempts = Math.Max(1, _retryPolicy.AttemptDelays.Length);
        var success = false;
        for (var i = 0; i <= maxAttempts; i++)
        {
          _retryAttempt = i;
          if (await ExecuteCommandAsync())
          {
            success = true;
            break;
          }
        }

        // All attempts failed
        if (!success)
          _processingCommand.ProcessResponse(null);
      }
      finally
      {
        CleanData();
      }
    }

    private async UniTask<bool> ExecuteCommandAsync()
    {
      try
      {
        var timeoutSec = _retryAttempt < _retryPolicy.AttemptDelays.Length
          ? _retryPolicy.AttemptDelays[_retryAttempt]
          : _retryPolicy.TimeoutSec;

        _cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSec));
        
        var requestJson = JsonConvert.SerializeObject(_processingCommand.RequestDto);

        var responseJson = await _fakeNetwork.CallServerMethodAsync(_processingCommand.MethodName, requestJson)
          .AttachExternalCancellation(_cancellation.Token);

        if (string.IsNullOrEmpty(responseJson))
          return false;

        var response = _processingCommand.DeserializeResponse(responseJson);
        _processingCommand.ProcessResponse(response);
        return true;
      }
      catch
      {
        return false;
      }
      finally
      {
        DisposeCancellation();
      }
    }

    private void CleanData()
    {
      DisposeCancellation();
      _retryAttempt = 0;
      _processingCommand = null;
    }

    private void DisposeCancellation()
    {
      _cancellation?.Dispose();
      _cancellation = null;
    }
  }
}