using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMicrophoneRecord : MonoBehaviour {
    AudioSource speakerAudio;
    string[] devices;
    bool isRecording = false;
    // Start is called before the first frame update
    void Start() {
        speakerAudio = GameObject.FindWithTag("speaker").GetComponent<AudioSource>();
        devices = Microphone.devices;

        if (devices.Length == 0) {
            Debug.Log("No microphone found");
            throw new System.Exception("No microphone found");
        }
        else {
            foreach (var device in devices) {
                Debug.Log("Microphone found: " + device);
            }
        }

        Debug.Log("Used Microphone: " + devices[2]);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isRecording) {
                Microphone.End(devices[2]);
                isRecording = false;
            } else {
                speakerAudio.clip = Microphone.Start(devices[2], true, 10, 44100);
                isRecording = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isRecording && speakerAudio.clip != null) {
            speakerAudio.Play();
        } else if (Input.GetKeyDown(KeyCode.R) && !isRecording && speakerAudio.clip == null) {
            Debug.Log("Audio clip is empty, press space to record");
        } else if (Input.GetKeyDown(KeyCode.R) && isRecording) {
            Debug.Log("Recording in progress, press space to stop");
        }
    }
}