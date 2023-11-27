using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField] private HeroMove2D _heroMove;
    [SerializeField] public Animator _animator;

    private static readonly int MoveHash = Animator.StringToHash("Walking");
    private static readonly int AttackHash = Animator.StringToHash("AttackNormal");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");

    private readonly int _idleStateHash = Animator.StringToHash("Idles");
    private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _deathStateHash = Animator.StringToHash("Die");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }
    public bool IsAttacking
    {
      get { return State == AnimatorState.Attack; }
    }

    public void Update() => 
      _animator.SetFloat(MoveHash, Math.Abs(_heroMove.MovementVector.x));

    public void PlayHit()
    {
      _animator.SetTrigger(HitHash);
    }

    public void PlayAttack()
    {
      _animator.SetTrigger(AttackHash);
    }

    public void PlayDeath()
    {
      _animator.SetTrigger(DieHash);
    }

    public void ResetToIdle()
    {
      _animator.Play(_idleStateHash, -1);
    }

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash)
    {
      StateExited?.Invoke(StateFor(stateHash));
    }

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
        state = AnimatorState.Idle;
      }
      else if (stateHash == _attackStateHash)
      {
        state = AnimatorState.Attack;
      }
      else if (stateHash == _walkingStateHash)
      {
        state = AnimatorState.Walking;
      }
      else if (stateHash == _deathStateHash)
      {
        state = AnimatorState.Died;
      }
      else
      {
        state = AnimatorState.Unknown;
      }

      return state;
    }
  }
}