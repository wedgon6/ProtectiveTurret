using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;

    public void AddMonue(int moneu)
    {
        _money.AddMoney(moneu);
    }
}
