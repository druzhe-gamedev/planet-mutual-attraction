using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] private float _checkRadius; // ������ �����, � ������� ���� ����������� ���� 
    private readonly float G = 6.67f * Mathf.Pow(10f, -11f); // �������������� ����������
    [SerializeField] private float _impulse; // ��������� ������� �������
    [SerializeField] private float _forceMultiplier; // ��������� ���� ���������� (����� ��� ������� ��������)

    private Rigidbody2D _rigidbody2D;

    private Collider2D[] _attractingObjects; // ����������� ���� ������������� ������

    [SerializeField] private GameObject _destroyedPlanet; // ����������� �������

    [Header("Start Impulse")]
    [SerializeField] private Vector3 _impulseDirection; // ������ ���������� ��������

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _rigidbody2D.AddForce(_impulseDirection * _impulse); // ����� ��������� �������
    }

    private void Update()
    {
        _attractingObjects = Physics2D.OverlapCircleAll(transform.position, _checkRadius); // ���� ����������� ���� ����������

        foreach(Collider2D attractingObject in _attractingObjects)
        {
            if(attractingObject.TryGetComponent(out Rigidbody2D rigidbody2D) && attractingObject.gameObject != gameObject) // ���� ������ ���� ������� 
            {
                float doubledDistance = Mathf.Pow(Vector2.Distance(transform.position, attractingObject.gameObject.transform.position), 2);
                float force = (_rigidbody2D.mass * rigidbody2D.mass) / doubledDistance * G * _forceMultiplier; // ����������� ���� �����������

                _rigidbody2D.AddForce(((transform.position - attractingObject.gameObject.transform.position).normalized * -force) * Time.deltaTime, ForceMode2D.Impulse); // ����������� ������ � ������� ������������ ����
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // ������������ � ������������
    {
        Instantiate(_destroyedPlanet, transform.position, transform.rotation); // ������ ������ ����������� �������

        Destroy(gameObject); // ������� ������ �������
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, _checkRadius); // ���������� ����������� �����, � ������� ���� ����������� ����
        Gizmos.DrawLine(transform.position, transform.position + _impulseDirection * _impulse * 0.0005f); // ���������� ����������� ������� ���������� ��������
    }
}
