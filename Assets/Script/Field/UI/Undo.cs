using System;

public class Undo
{
    Action onUndo;

    public Undo(Action onUndo)
    {
        this.onUndo = onUndo;
    }

    public void Excute()
    {
        onUndo?.Invoke();
    }
}
