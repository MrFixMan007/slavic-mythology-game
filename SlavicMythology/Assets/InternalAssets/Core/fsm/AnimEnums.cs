using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Animation
{
    public enum AnimEnums
    {
        IdleFront,
        IdleBack,
        IdleLeft,
        IdleRight,

        AttackFront,
        AttackBack,
        AttackLeft,
        AttackRight,

        WalkFront,
        WalkBack,
        WalkLeft,
        WalkRight,
    }

    public class AnimState
    {
        public AnimEnums AnimEnums { get; private set; }
        public string trigerName { get; private set; }

        public AnimState(AnimEnums animEnums)
        {
            AnimEnums = animEnums;
            trigerName = animEnums.ToString();
        }
    }

    public class AnimFsm
    {
        private AnimState _currentState;
        private Animator _animator;
        private List<AnimState> _states;

        public AnimFsm(AnimState currentState, Animator animator, List<AnimState> states)
        {
            _currentState = currentState;
            _animator = animator;
            _states = states;
        }

        public AnimFsm(Animator animator, List<AnimState> states)
        {
            _currentState = states[0];
            _animator = animator;
            _states = states;
        }

        public static AnimFsm CreateSampleAnimFsm(Animator animator)
        {
            List<AnimState> animStates = new List<AnimState>
            {
                new(animEnums: AnimEnums.IdleFront),
                new(animEnums: AnimEnums.IdleBack),
                new(animEnums: AnimEnums.IdleLeft),
                new(animEnums: AnimEnums.IdleRight),

                new(animEnums: AnimEnums.AttackFront),
                new(animEnums: AnimEnums.AttackBack),
                new(animEnums: AnimEnums.AttackLeft),
                new(animEnums: AnimEnums.AttackRight),

                new(animEnums: AnimEnums.WalkFront),
                new(animEnums: AnimEnums.WalkBack),
                new(animEnums: AnimEnums.WalkLeft),
                new(animEnums: AnimEnums.WalkRight),
            };
            return new AnimFsm(animator: animator, states: animStates);
        }
        
        public static AnimFsm CreatePlayerSampleAnimFsm(Animator animator)
        {
            List<AnimState> animStates = new List<AnimState>
            {
                new(animEnums: AnimEnums.IdleFront),
                new(animEnums: AnimEnums.IdleBack),
                new(animEnums: AnimEnums.IdleLeft),
                new(animEnums: AnimEnums.IdleRight),

                new(animEnums: AnimEnums.AttackFront),
                new(animEnums: AnimEnums.AttackBack),
                new(animEnums: AnimEnums.AttackLeft),
                new(animEnums: AnimEnums.AttackRight),

                new(animEnums: AnimEnums.WalkFront),
                new(animEnums: AnimEnums.WalkBack),
                new(animEnums: AnimEnums.WalkLeft),
                new(animEnums: AnimEnums.WalkRight),
            };
            return new AnimFsm(animator: animator, states: animStates);
        }

        public void SetState(AnimEnums animEnums)
        {
            if (_currentState.AnimEnums == animEnums)
                return;

            foreach (var state in _states)
            {
                if (state.AnimEnums == animEnums)
                {
                    _currentState = state;
                    _animator.SetTrigger(state.trigerName);
                }
                else
                {
                    _animator.ResetTrigger(state.trigerName);
                }
            }
        }
    }
}