using UnityEngine;
using System.Collections;
// Wir loggen mit log4net
using log4net;

/// <summary>
/// Bewegung eines GameObjects mit Hilfe der Cursortasten 
/// innerhalb eines Rechecks in x und z-Koordinaten. Die y-Koordinate 
/// des bewegten Objekts wird abgefragt und nicht verändert.
/// 
/// Die Koordinaten werden mit log4net auf die Konsole
/// oder in eine Datei ausgegeben, falls der Logging-Level
/// INFO ist.
/// </summary>
public class PlayerControlWithLogs : MonoBehaviour
{
    /// <summary>
    /// Grenzen in x und z
    /// </summary>
	public const float MIN_X = -15,
	                   MAX_X = 15,
	                   MIN_Z = -10,
	                   MAX_Z = 10;
    
    /// <summary>
    /// y-Koordinate des bewegten Ojekts. Wird in Awake abgefragt.
    /// </summary>
    private float m_y;
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
	private float m_speed = 20.0F;

    /// <summary>
    /// Instanz eines Loggers
    /// </summary>
    private static readonly ILog Log = LogManager.GetLogger(typeof(PlayerControlWithLogs));

    /// <summary>
    /// Initialisierung
    /// 
    /// Wir fragen die y-Koordinate des GameObjects ab,
    /// die von uns nicht verändert wird.
    /// Wir benötigen diesen Wert für die Translationsmatrix,
    /// mit der wir die Bewegung durchführen.
    private void Awake()
    {
		m_y = transform.position.y;
    }

    private void Start()
    {
        Log.Debug(">>> Start");
        Log.Debug(" y-Wert des Players: " + m_y.ToString());
        Log.Debug("<<< Start");
    }

    /// <summary>
    /// Bewegung in FixedUpdate
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void FixedUpdate ()
    {
        Log.Debug(">>> FixedUpdate");
		KeyboardMovement();
		CheckBounds();
        Log.Debug("<<< FixedUpdate");
	}
	
    /// <summary>
    /// Abfragen der Achsen Horizontal und Vertical (das sind zum Beispiel
    /// die Cursortasten in Unity) und Translation an Hand dieser Eingaben.
    /// </summary>
	private void KeyboardMovement(){
		float dx = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
		float dz = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
		transform.Translate( new Vector3(dx, m_y, dz) );		
	}
	
    /// <summary>
    /// Überprüfen, ob die Grenzen eingehalten werden.
    /// </summary>
	private void CheckBounds()
    {
        Log.Debug(">>> CheckBounds");
        float x = transform.position.x;
		float z = transform.position.z;
		x = Mathf.Clamp(x, MIN_X, MAX_X);
		z = Mathf.Clamp(z, MIN_Z, MAX_Z);
		
		transform.position = new Vector3(x, m_y, z);
        Log.Info("Neue Position des Players: " + transform.position.ToString());
        Log.Debug("<<< CheckBounds");
	}
}
