using ExternalPropertyAttributes;
using UnityEngine;

namespace Dustyroom {
public class NoiseMotion : MonoBehaviour {
    [BoxGroup("Position Noise"), Label("Enable")]
    public bool enablePositionNoise = true;

    [BoxGroup("Rotation Noise"), Label("Enable")]
    public bool enableRotationNoise = true;

    [BoxGroup("Position Noise"), ShowIf(nameof(enablePositionNoise)), Label("\tFrequency"), HorizontalLine()]
    public float positionFrequency = 0.2f;

    [BoxGroup("Rotation Noise"), ShowIf(nameof(enableRotationNoise)), Label("\tFrequency"), HorizontalLine()]
    public float rotationFrequency = 0.2f;

    [BoxGroup("Position Noise"), ShowIf(nameof(enablePositionNoise)), Label("\tAmplitude")]
    public float positionAmplitude = 0.5f;

    [BoxGroup("Rotation Noise"), ShowIf(nameof(enableRotationNoise)), Label("\tAmplitude")]
    public float rotationAmplitude = 10.0f;

    [BoxGroup("Position Noise"), ShowIf(nameof(enablePositionNoise)), Label("\tScale")]
    public Vector3 positionScale = Vector3.one;

    [BoxGroup("Rotation Noise"), ShowIf(nameof(enableRotationNoise)), Label("\tScale")]
    public Vector3 rotationScale = new Vector3(1, 1, 0);

    [BoxGroup("Position Noise"), ShowIf(nameof(enablePositionNoise)), Label("\tSeed")] [Range(0, 8)]
    public int positionNoiseSeed = 3;

    [BoxGroup("Rotation Noise"), ShowIf(nameof(enableRotationNoise)), Label("\tSeed")] [Range(0, 8)]
    public int rotationNoiseSeed = 3;

    static int _seed = 0;

    Vector3 _initialPosition;
    Quaternion _initialRotation;
    float[] _time;

    void Start() {
        Random.InitState(_seed++);
        _time = new float[6];
        for (var i = 0; i < 6; i++) {
            _time[i] = Random.Range(-10000.0f, 0.0f);
        }
    }

    void OnEnable() {
        var t = transform;
        _initialPosition = t.localPosition;
        _initialRotation = t.localRotation;
    }

    void Update() {
        var dt = Time.deltaTime;

        if (enablePositionNoise) {
            for (var i = 0; i < 3; i++) {
                _time[i] += positionFrequency * dt;
            }

            var n = new Vector3(
                Mathf.PerlinNoise(_time[0], positionNoiseSeed),
                Mathf.PerlinNoise(_time[1], positionNoiseSeed),
                Mathf.PerlinNoise(_time[2], positionNoiseSeed));

            n = Vector3.Scale(n, positionScale);
            n *= positionAmplitude;

            transform.localPosition = _initialPosition + n;
        }

        if (enableRotationNoise) {
            for (var i = 0; i < 3; i++) {
                _time[i + 3] += rotationFrequency * dt;
            }

            var n = new Vector3(
                Mathf.PerlinNoise(_time[3], rotationNoiseSeed),
                Mathf.PerlinNoise(_time[4], rotationNoiseSeed),
                Mathf.PerlinNoise(_time[5], rotationNoiseSeed));

            n = Vector3.Scale(n, rotationScale);
            n *= rotationAmplitude;

            transform.localRotation = Quaternion.Euler(n) * _initialRotation;
        }
    }
}
}