using UnityEngine;

public interface IHealth
{
    float Health { get; set; }
    float Oxygen { get; set; }

    void ReduceOxygen(float damage);
    void TakeDamage(float damage);
    void Death();
}
