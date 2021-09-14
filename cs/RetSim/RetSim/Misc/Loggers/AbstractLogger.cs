namespace RetSim.Misc.Loggers;

public abstract class AbstractLogger
{
    public abstract void Log(string message);

    public abstract void DisableInput();
    public abstract void EnableInput();
}