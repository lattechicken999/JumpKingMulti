using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRecorder : MonoBehaviour
{
    List<IPlayerCommand> commands = new List<IPlayerCommand>();
    bool isRecording = false;

    public void StartRecording()
    {
        commands.Clear();
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void Record(IPlayerCommand command)
    {
        if (isRecording == true)
        {
            commands.Add(command);
        }
    }
    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }
    IEnumerator PlayRoutine()
    {
        foreach (var cmd in commands)
        {
            yield return cmd.Replay();
        }
    }
}
