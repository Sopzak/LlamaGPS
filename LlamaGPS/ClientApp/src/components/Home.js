import React, { Component, useState, useEffect } from 'react';
import Unity, { UnityContent, UnityContext } from 'react-unity-webgl'
import { Footer } from './Footer';

export class Home extends Component {
    /*static unityContent = new UnityContent(
        "/LlamaGPS/Build/Unityloader.js", "/LlamaGPS/Build/LlamaGPS.json"
    );*/

    render() {

        const unityContent = new UnityContent(
            '../../Build/LlamaGPS.json',
            '../../Build/Unityloader.js'

        )

        return (
            <div>
                <p>Game</p>
                <Unity unityContent={unityContent} width="100%" height="100%" />
                <p>Loading {100} percent...</p>
                <Footer/>
            </div>
        )
    }
}
