using UnityEngine;

public class PourableLiquid : MonoBehaviour
{
    public enum PourType { Water, Coffee }

    public PourType pourType;
    public ParticleSystem pourParticles;

    public float pourAngle = 120f;

    private bool isPouring = false;

    void Update()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        if (angle > pourAngle)
        {
            if (!isPouring)
            {
                isPouring = true;

                if (pourParticles != null)
                    pourParticles.Play();
            }
        }
        else
        {
            if (isPouring)
            {
                isPouring = false;

                if (pourParticles != null)
                    pourParticles.Stop();
            }
        }
    }

    public bool IsPouring()
    {
        return isPouring;
    }
}