public class GameStateMashine : StateMashine
{
    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            YandexGamesSdk.GameReady();
#endif
    }

    private void Start()
    {
        EnterState(_firstState);
    }

    private void Update()
    {
        UpdateStateStatus();
    }
}