using System;
using Client.Controller.TurnsProcess;
using Client.Model.GameCommands;
using Shared.SharedModel.Dto.LaunchMatch;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Client.Controller.InputHandle
{
  public class LaunchMatchButtonHandler : ButtonCommandHandlerBase
  {
#if UNITY_EDITOR
    [SerializeField] private SceneAsset _gameplaySceneAsset;
#endif
    
    [SerializeField] [HideInInspector] private string _gameplaySceneName;
    
    protected override IGameCommand GetCommand(Guid processingRequestGuid)
    {
      return new LaunchMatchCommand(new LaunchMatchRequestDto
      {
        RequestId = processingRequestGuid
      }, AppModel, TurnEventsFeed)
      .SetOnOk(_ =>
      {
        SceneManager.LoadScene(_gameplaySceneName);
      })
      .SetOnError(() =>
      {
        // TODO: handle error
      });
    }
    
    

#if UNITY_EDITOR
    private void OnValidate()
    {
      if (_gameplaySceneAsset != null)
        _gameplaySceneName = _gameplaySceneAsset.name;
    }
#endif
  }
}