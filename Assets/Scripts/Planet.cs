using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private float _checkRadius; // радиус круга, в котором ищем космические тела 
    private readonly float G = 6.67f * Mathf.Pow(10f, -11f); // гравитационна€ посто€нна€
    [SerializeField] private float _impulse; // начальный импульс планеты
    [SerializeField] private float _forceMultiplier; // множитель силы прит€жени€ (иначе идЄт слишком медленно)

    private Rigidbody2D _rigidbody2D;

    private Collider2D[] _attractingObjects; // космические тела прит€гивающие объект

    [SerializeField] private GameObject _destroyedPlanet; // разрушенна€ планета

    [Header("Start Impulse")]
    [SerializeField] private Vector3 _impulseDirection; // вектор стартового импульса

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _rigidbody2D.AddForce(_impulseDirection * _impulse); // задаЄм начальный импульс
    }

    private void Update()
    {
        _attractingObjects = Physics2D.OverlapCircleAll(transform.position, _checkRadius); // ищем космические тела поблизости

        foreach(Collider2D attractingObject in _attractingObjects)
        {
            if(attractingObject.TryGetComponent(out Rigidbody2D rigidbody2D) && attractingObject.gameObject != gameObject) // берЄм твЄрдое тело объекта 
            {
                float doubledDistance = Mathf.Pow(Vector2.Distance(transform.position, attractingObject.gameObject.transform.position), 2);
                float force = (_rigidbody2D.mass * rigidbody2D.mass) / doubledDistance * G * _forceMultiplier; // расчитываем силу воздействи€

                _rigidbody2D.AddForce(((transform.position - attractingObject.gameObject.transform.position).normalized * -force) * Time.deltaTime, ForceMode2D.Impulse); // прит€гиваем объект к другому космическому телу
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // столкновение с преп€тствием
    {
        Instantiate(_destroyedPlanet, transform.position, transform.rotation); // создаЄм объект разрушенной планеты

        Destroy(gameObject); // удал€ем объект планеты
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _checkRadius); // визуальное отображение круга, в котором ищем космические тела
        Gizmos.DrawLine(transform.position, transform.position + _impulseDirection * _impulse * 0.0005f); // визуальное отображение вектора начального импульса
    }
}
