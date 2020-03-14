using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AgoraUiBehaviorScript : MonoBehaviour
{
    public TMP_InputField usernameField, passwordField;

    public Button authButton;

    public TMP_Text tokenText;

    public Button requestButton;

    public TMP_Text requestText;

    private AgoraClient client;

    public async void buttonPress()
    {
        var authResponse = await client.authenticate(usernameField.text, passwordField.text);

        tokenText.text = authResponse;
    }

    public async void requestPress()
    {
        var response = await client.GetJsonWithAuth("/patient/sessions");

        requestText.text = await response.Content.ReadAsStringAsync();
    }

    // Start is called before the first frame update
    void Start()
    {
        client = new AgoraClient();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
