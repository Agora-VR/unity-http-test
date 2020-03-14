using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UiBehavior : MonoBehaviour
{
    public Button requestButton;

    public TMP_Text messageText;

    private Dictionary<string, Action> functionMap;

    public async void getClick()
    {
        var myGetString = await RestClient.GetJsonString("https://jsonplaceholder.typicode.com/todos/1");

        messageText.text = myGetString;
    }

    public async void postClick()
    {
        var postPayload = new Dictionary<string, object>();

        postPayload.Add("title", "foo");
        postPayload.Add("body", "bar");
        postPayload.Add("userId", 1);

        var myPostString = await RestClient.PostJsonString(
            "https://jsonplaceholder.typicode.com/posts", postPayload);

        messageText.text = myPostString;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
