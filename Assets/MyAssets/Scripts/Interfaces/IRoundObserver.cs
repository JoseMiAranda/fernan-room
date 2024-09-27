public interface IRoundObserver
{
    void OnRoundStarted(int round);
    void OnRoundCleared(int round);
}