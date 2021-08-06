import React, { Component } from 'react';
import Unity, { UnityContent } from 'react-unity-webgl'

export class Home extends Component {
    render() {

        const unityContent = new UnityContent(
            '../../Build/LlamaGPS.json',
            '../../Build/Unityloader.js'
        )

        return (
            <div>
                <p>Game</p>
                <Unity unityContent={unityContent} width="100%" height="100%" />
            </div>
        )
    }
}
