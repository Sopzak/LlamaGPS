import React, { Component, useState, useEffect } from 'react';
import Unity, { UnityContent, UnityContext } from 'react-unity-webgl'

export class Home extends Component {
    /*static unityContent = new UnityContent(
        "/LlamaGPS/Build/Unityloader.js", "/LlamaGPS/Build/LlamaGPS.json"
    );*/

    render() {
        /*var unityContext = new UnityContext({
            loaderUrl: "/LlamaGPS/Build/Unityloader.js",
        });
        return (
            <div>
                <h1>Hellcome llama from LlamaZOO</h1>
                <p>This is a exemple of what I can do in your team.</p>
                <Unity unityContext={unityContext} />
            </div>
        );*/

        const unityContent = new UnityContent(
            '../../Build/LlamaGPS.json',
            '../../Build/Unityloader.js'

        )

        return (
            <div>
                <p>Game</p>
                <Unity unityContent={unityContent} width="100%" height="100%" />
                <p>Loading {100} percent...</p>
            </div>
        )
    }
}
