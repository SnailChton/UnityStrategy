using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    public Light lightDir;

    private ParticleSystem _ps;
    private bool _isRain = false;

    private void Start() {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update() {
        if (_isRain && lightDir.intensity > 0.25f) {
            LightInensity(-1);
        } else if (!_isRain && lightDir.intensity < 0.5f) {
            LightInensity(1);
        }
    }

    private void LightInensity(int mult) {
        lightDir.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather() {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(40f, 120f));

            if (_isRain){
                _ps.Stop();
            } else {
                _ps.Play();
            }
            _isRain = !_isRain;
        }
    }
}
