using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Waves;
    private List<Animator> WavesAnimations = new List<Animator>();
    public static readonly int Curve = Animator.StringToHash("Basic_Enemy");
    public static readonly int Straight = Animator.StringToHash("Straight_Enemy");
    private readonly string[] animations = { "Straight_Enemy", "Basic_Enemy" };
    private List<List<Enemy>> waveEnemies = new List<List<Enemy>>();
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < Waves.Capacity; i++)
        {
            waveEnemies.Add(new List<Enemy>());

            WavesAnimations.Add(Waves[i].GetComponent<Animator>());
            for (int j = 0; j < Waves[i].transform.childCount; j++)
            {
                waveEnemies[i].Add(Waves[i].transform.GetChild(j).GetComponent<Enemy>());
            }
        }
    }
    void Start()
    {
        InvokeRepeating(nameof(StartWave), 4f, 5f);
    }

    private void StartWave()
    {
        notShoot();
        int rand = Random.Range(0, Waves.Capacity);
        Waves[rand].SetActive(true);

        WavesAnimations[rand].Play(animations[rand], -1, 0f);

        for (int i = 0; i < Waves[rand].transform.childCount; i++)
        {
            bool activate = false;
            bool disparar = false;

            int rand2 = Random.Range(0, 3);
            int rand3 = Random.Range(0, 3);
            if (rand2 >= 1) { activate = true; }
            Waves[rand].transform.GetChild(i).gameObject.SetActive(activate);
            if(rand3 != 0) { disparar = true; }
            waveEnemies[rand][i].disparar = disparar;
        }

    }

    private void notShoot()
    {
        for (int i = 0; i < Waves.Capacity; i++)
        {
            for (int j = 0; j < Waves[i].transform.childCount; j++)
            {
                waveEnemies[i][j].disparar = false;
            }
        }
    }
}
