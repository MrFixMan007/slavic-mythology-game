using UnityEngine;

public class AtmosphereEffects : MonoBehaviour
{
    [Header("Falling Leaves Settings")]
    public ParticleSystem leavesParticles;
    public Color leavesColor = new Color(0.5f, 0.3f, 0.1f); // Цвет листьев (осенний)
    public float leavesSize = 0.5f;
    public float leavesSpeed = 1.0f;

    [Header("Dust Settings")]
    public ParticleSystem dustParticles;
    public Color dustColor = Color.gray; // Цвет пыли
    public float dustSize = 0.1f;
    public float dustSpeed = 0.2f;

    [Header("Magic Light Settings")]
    public ParticleSystem magicParticles;
    public Gradient magicColorGradient; // Градиент цвета для магического света
    public float magicSize = 0.4f;
    public float magicSpeed = 0.7f;

    void Start()
    {
        SetupFallingLeaves();
        SetupDust();
        SetupMagicLight();
    }

    // Настройка падающих листьев
    void SetupFallingLeaves()
    {
        var main = leavesParticles.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(5.0f, 10.0f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(leavesSpeed * 0.5f, leavesSpeed);
        main.startSize = new ParticleSystem.MinMaxCurve(leavesSize * 0.5f, leavesSize);
        main.gravityModifier = 0.2f;
        main.startColor = leavesColor;

        var emission = leavesParticles.emission;
        emission.rateOverTime = 10f;

        var shape = leavesParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 15f;
        shape.radius = 5f;

        var rotation = leavesParticles.rotationOverLifetime;
        rotation.enabled = true;
        rotation.z = new ParticleSystem.MinMaxCurve(10f, 20f); // Кручение при падении

        leavesParticles.Play();
    }

    // Настройка пыли
    void SetupDust()
    {
        var main = dustParticles.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(2.0f, 4.0f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(dustSpeed * 0.5f, dustSpeed);
        main.startSize = new ParticleSystem.MinMaxCurve(dustSize * 0.5f, dustSize);
        main.startColor = dustColor;

        var emission = dustParticles.emission;
        emission.rateOverTime = 50f;

        var shape = dustParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 15f;
        shape.radius = 3f;

        var noise = dustParticles.noise;
        noise.enabled = true;
        noise.strength = new ParticleSystem.MinMaxCurve(0.1f, 0.2f); // Легкое мерцание

        dustParticles.Play();
    }

    // Настройка магического света
    void SetupMagicLight()
    {
        var main = magicParticles.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(1.0f, 2.0f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(magicSpeed * 0.3f, magicSpeed);
        main.startSize = new ParticleSystem.MinMaxCurve(magicSize * 0.2f, magicSize);
        main.startColor = new ParticleSystem.MinMaxGradient(magicColorGradient);

        var emission = magicParticles.emission;
        emission.rateOverTime = 30f;

        var shape = magicParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 1.0f;

        magicParticles.Play();
    }
}
