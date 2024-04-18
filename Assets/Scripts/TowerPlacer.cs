using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tower {
	public GameObject Bullet;
	public int Damage { get; set; }
	public float TimeToReload { get; set; }
	public float ReloadTime { get; set; }
	public Vector3Int Vector { get; set; }
	public Tile Tile { get; set; }
	public int Level { get; set; }
	public float CurrentAngle { get; set; }
	public Vector3 WorldPosition { get; set; }
}

public class TowerPlacer : MonoBehaviour {
	public Tilemap mapPlatforms;
	public Tilemap mapTowers;
	public Tile Tower;
	public Tile TileBase;
	private Camera mainCamera;
	float timeTill = 0.0f;
	List<Tower> towers;

	void Start() {
		mainCamera = Camera.main;
		towers = new List<Tower>();
	}

	void Shoot(Collider2D target, Tower tower) {
		Vector3 startPosition = tower.WorldPosition;
		Vector3 targetPosition = target.transform.position;
		startPosition.z = tower.Bullet.transform.position.z;
		targetPosition.z = tower.Bullet.transform.position.z;

		GameObject newBullet = (GameObject)Instantiate(tower.Bullet);
		newBullet.transform.position = startPosition;
		BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
		bulletComp.target = target.gameObject;
		bulletComp.startPosition = startPosition;
		bulletComp.targetPosition = targetPosition;
		bulletComp.damage = tower.Damage;
	}

	void Update() {
		timeTill += Time.deltaTime;

		if (Input.GetMouseButtonDown(0)) {
			Vector3 click = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int cellClick = mapPlatforms.WorldToCell(click);
			if (mapPlatforms.GetTile(cellClick) == TileBase) {
				mapTowers.SetTile(cellClick, Tower);
			}
			towers.Add(new Tower {
				Vector = cellClick,
				Tile = mapTowers.GetTile(cellClick) as Tile,
				Level = 1,
				WorldPosition = click,
				Bullet = Resources.Load<GameObject>("Bullet"),
				Damage = 20,
				TimeToReload = 3,
				ReloadTime = 3,
			});
		}

		if (timeTill > 2) {
			foreach (var tower in towers) {
				tower.CurrentAngle += 45;
				Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f,
				tower.CurrentAngle), Vector3.one);
				mapTowers.SetTransformMatrix(tower.Vector, matrix);
			}
			timeTill = 0.0f;
		}

		foreach (var tower in towers) {
			if (tower.TimeToReload == 0) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(tower.WorldPosition.x, tower.WorldPosition.y), 2);
				foreach (Collider2D collider in colliders) {
					Shoot(collider, tower);
					tower.TimeToReload = tower.ReloadTime;
					break;
				}
			} else {
				tower.TimeToReload = Mathf.Max(0, tower.TimeToReload - Time.deltaTime);
			}
		}
	}
}



