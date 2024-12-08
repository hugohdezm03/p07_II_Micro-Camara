using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCameraRecord : MonoBehaviour {
    Material screenMaterial;
    Material originalMaterial;
    // int captureCounter = 1;
    string path = Application.dataPath + "/SnapShots/";
    WebCamTexture screenTexture;
    bool isRecording = false;
    WebCamDevice[] devices;

    void Start() {
        screenMaterial = GameObject.FindWithTag("screen").GetComponent<Renderer>().material;
        originalMaterial = screenMaterial;
        screenMaterial.shader = Shader.Find("Unlit/Texture");

        if (!System.IO.Directory.Exists(path)) {
            System.IO.Directory.CreateDirectory(path);
            Debug.Log("Directory created: " + path);
        }

        devices = WebCamTexture.devices;
        if (devices.Length == 0) {
            Debug.Log("No camera found");
            throw new System.Exception("No camera found");
        } else {
            foreach (var device in devices) {
                Debug.Log("Camera found: " + device.name);
            }
        }

        screenTexture = new WebCamTexture(devices[0].name);
        screenMaterial.mainTexture = screenTexture;
        screenTexture.Play();
        isRecording = true;
    }


    void Update() {
        if (Input.GetKeyDown("s")) {
            // Asignar a la mainTexture del material lo que capta la cámara. La captura se inicia con nombre_variable__webcamtexture.Start()
            if (isRecording) {
                screenTexture.Pause();
                isRecording = false;
            } else {
                screenTexture.Play();
                isRecording = true;
            }
        }

        if (Input.GetKeyDown("p")) {
            // parar la captura de video
            screenTexture.Stop();
            screenMaterial = originalMaterial;
        }

        if (Input.GetKeyDown("x")) {
            // Tomar forograma
            Texture2D snapshot = new Texture2D(screenTexture.width, screenTexture.height);
            snapshot.SetPixels(screenTexture.GetPixels());
            snapshot.Apply();
            string name = System.DateTime.Now.ToString();
            name = name.Replace("/", "-");
            name = name.Replace(":", "_");
            System.IO.File.WriteAllBytes(path + "snapshot_" + name + ".png", snapshot.EncodeToPNG());
            Debug.Log("Snapshot taken");
        }
    }
}
