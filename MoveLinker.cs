using System.Collections.Generic;
using UnityEngine;

namespace ToyGame.FSM
{
	public class MoveLinker : MonoBehaviour
	{
		[SerializeField] private List<EnemyStateType> weights = new();
		private Enemy enemy => GetComponentInParent<Enemy>();
		public EnemyStateType? LinkNextMove() {
			int index = Random.Range(0, weights.Count);
			if (weights.Count == 0 || CheckLinkTooLong())
				return null;
			return weights[index];
        }

		private bool CheckLinkTooLong()
		{
			return enemy.attackCount >= enemy.maxAttackCount;
		}
	}
}