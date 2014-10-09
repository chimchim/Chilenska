using UnityEngine;
using System.Collections;
using Game;
using Game.GEntity;
using Game.Component;
using Game.System;

public class GameUnity : MonoBehaviour 
{
    public Transform trans;
	GameManager game = new GameManager();
    private int entity;
	void Start () 
	{
		Entity ent = new Entity();
		game.Entities.addEntity(ent);
        ent.AddComponent(GTransform.Make(ent.ID, Vector3.zero, new Vector2(0.2f, 0.5f)));
		ent.AddComponent(GRawInput.Make(ent.ID));
        ent.AddComponent(ActionQueue.Make(ent.ID));
        ent.AddComponent(GCollisionMask.Make(ent.ID));
        ent.AddComponent(GMovement.Make(ent.ID));
        ent.AddComponent(GBoxCollider2D.Make(ent.ID, new Vector2(0.2f, 0.5f),
                                                     new Vector2(0, -0.1f), new Vector2(0.2f, 0.5f)));
        entity = ent.ID;
        game.Systems.CreateSystems();
        game.Systems.AddEntity(0, ent.ID);
        game.Systems.AddEntity(1, ent.ID);
        game.Initiate();
	}
	
	// Update is called once per frame
	void Update () 
	{
       // game.Update(Time.deltaTime);
       // trans.position = game.Entities.GetComponentOf<GTransform>(entity)._position;
	}

    private void addSystems(GameManager game)
    { 
        //game.Systems.
    }
}
