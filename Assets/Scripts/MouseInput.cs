using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ParticleSystem))] // Used because I call GetComponent<ParticleSystem>()
public class MouseInput : MonoBehaviour
{
    #region Cached Components
    // The effects that should be played whenever the player clicks somewhere.
    private ParticleSystem m_Particles;
    private Score m_Score;
    #endregion

    #region First Time Initialization and Set Up
    private void Awake()
    {
        m_Particles = GetComponent<ParticleSystem>();
        m_Score = GameObject.Find("Score").GetComponent<Score>();
    }
    #endregion

    #region Main Updates

    private Vector2 lastMousePoint = Vector2.zero;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("Mouse Down");
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            wp.z = 0;

            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            emitParams.position = wp;
            emitParams.velocity = ((Vector2)Input.mousePosition - lastMousePoint).normalized * 2;
            emitParams.applyShapeToPosition = true;
            m_Particles.Emit(emitParams, 100);

            Collider2D[] allOverlaps = Physics2D.OverlapCircleAll(wp, 0.2f);
            // Debug.Log(string.Format("Hit {0} objects", allOverlaps.Length));
            foreach (Collider2D col in allOverlaps)
            {
                Target t = col.GetComponent<Target>();
                if (t != null)
                {
                    m_Score.AddScore(t.Hit());

                    ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
                    main.startColor = col.GetComponent<SpriteRenderer>().color;
                }
            }
        }
        lastMousePoint = Input.mousePosition;
    }
    #endregion
}
