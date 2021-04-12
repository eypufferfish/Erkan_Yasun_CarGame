using Mobge.CarGame.ErkanYasun.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Transform carPrefab;
    [SerializeField]
    private Level levelData;
    // Start is called before the first frame update
    void Start()
    {
        if (levelData != null) {
            GameArea gameArea = levelData.GameArea;
            List<CarPathPair> carPathPairs =  gameArea.CarPathPairs;
            if (carPathPairs != null) {
                foreach (CarPathPair carPathPair in carPathPairs) {
                    Car car = carPathPair.Car;
                    Path path = carPathPair.Path;
                    InstantiateCar(path.Entrance, car);
                }
            
            }



        }
        
    }




    private Transform InstantiateCar(Vector2 aEntrance, Car aCarData)
    {

        carPrefab.GetComponent<SpriteRenderer>().sprite = aCarData.Sprite;
        return Instantiate(carPrefab, aEntrance, Quaternion.Euler(0f, 0f, 90));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
