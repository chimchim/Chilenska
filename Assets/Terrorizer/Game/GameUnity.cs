using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.GEntity;
using Game.Component;
using Game.System;

public class GameUnity : MonoBehaviour 
{
    public GameObject trans;
	GameManager game = new GameManager();
    public GameObject camera;
    public Dictionary<int, Transform> entityDic = new Dictionary<int, Transform>();
    private int entity;
	void Start () 
	{
		Entity ent = new Entity();
		game.Entities.addEntity(ent);
        ent.AddComponent(GTransform.Make(ent.ID, Vector3.zero, new Vector2(0.4f, 1f)));
		ent.AddComponent(GRawInput.Make(ent.ID));
        ent.AddComponent(ActionQueue.Make(ent.ID));
        ent.AddComponent(GCollisionMask.Make(ent.ID));
        ent.AddComponent(GMovement.Make(ent.ID));
        ent.AddComponent(GBoxCollider2D.Make(ent.ID, new Vector2(0.2f, 0.5f),
                                                     new Vector2(0, -0.1f), new Vector2(0.2f, 0.5f)));
        GameObject clone = Instantiate(trans, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        camera.transform.parent = clone.transform;
        entity = ent.ID;
        entityDic.Add(ent.ID, clone.transform);
        game.Systems.CreateSystems();
        game.Systems.AddEntity(0, ent.ID);
        game.Systems.AddEntity(1, ent.ID);
        game.Initiate();
        createAI(game);
	}
	
	// Update is called once per frame
	void Update () 
	{
        game.Update(Time.deltaTime);
        foreach (int ent in entityDic.Keys)
        {
            entityDic[ent].position = game.Entities.GetComponentOf<GTransform>(ent)._position;
        }
	}

    private void createAI(GameManager game)
    {
        for (int i = 0; i < 10; i++)
        {
            Entity ent = new Entity();
            game.Entities.addEntity(ent);
            ent.AddComponent(GTransform.Make(ent.ID, Vector3.zero, new Vector2(1f, 1f)));
            ent.AddComponent(GRawInput.Make(ent.ID));
            ent.AddComponent(ActionQueue.Make(ent.ID));
            ent.AddComponent(GCollisionMask.Make(ent.ID));
            ent.AddComponent(GMovement.Make(ent.ID));
            ent.AddComponent(GBoxCollider2D.Make(ent.ID, new Vector2(0.2f, 0.5f),
                                                         new Vector2(0, -0.1f), new Vector2(0.2f, 0.5f)));

            GameObject clone = Instantiate(trans, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            entityDic.Add(ent.ID, clone.transform);
            game.Systems.AddEntity(1, ent.ID);
        }
    }

}
