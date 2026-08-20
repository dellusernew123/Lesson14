using System.Collections;
using _Project.Scripts.Gameplay.Core.EntitiesCore;
using _Project.Scripts.Gameplay.Features;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private TestGameplay _testGameplay;

    [SerializeField] private Transform _playerStartPosition;
    private EntitiesLifeContext _entitiesLifeContext = new();

    private void Awake()
    {
        StartCoroutine(Initialize());
    }

    private void Update()
    {
        _entitiesLifeContext?.Update(Time.deltaTime);
        _testGameplay?.Update();
    }

    private IEnumerator Initialize()
    {
        _testGameplay = new TestGameplay(_playerStartPosition, _entitiesLifeContext);
        _testGameplay.Initialize();

        yield return Run();
    }

    private IEnumerator Run()
    {
        _testGameplay.Run();

        yield return null;
    }
}