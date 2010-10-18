using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lampa.Objects;
using Lampa;
namespace Lampa
{
    public class StateMachine
    {
        protected List<State> stateList = new List<State>();
        public bool ran = false;
        public bool finished = false;
        private bool cycleStates = false;
        private bool running = false;
        private int activeState = -1;
        protected State currentState = null;
        private Stage currentStage = Stage.Enter;
        private bool autoStart = true;

        public StateMachine(bool autoStart)
        {
            this.autoStart = autoStart;
        }
        public void ClearStates()
        {
            stateList.Clear();
        }

        public void Start()
        {
            if (stateList.Count > 0)
            {
                activeState = 0;
                currentState = stateList[activeState];
                currentStage = Stage.Enter;
                running = true;
                finished = false;
            }
        }

        public bool Running()
        {
            if (!running || currentState == null || finished)
            {
                return false;
            }
            return true;
        }

        public void RemoveState(State state)
        {
            stateList.Remove(state);
        }

        public void SetLooped(bool looped)
        {
            cycleStates = looped;
        }

        public void AddState(State state)
        {
            stateList.Add(state);

            if (stateList.Count == 1)
            {
                activeState = 0;
                currentState = stateList[activeState];
                currentStage = Stage.Enter;
                running = true;
            }
        }

        public void Draw()
        {
            if (stateList.Count == 0)
                return;

            if (currentState != null)
                currentState.Draw(currentStage);
        }

        public void Update(GameTime gameTime)
        {
            if (stateList.Count == 0)
                return;

            // Loop?
            if (activeState == -1 && currentState == null)
            {
                finished = true;
                if (cycleStates || (!running && !ran && autoStart))
                {
                    activeState = 0;
                    currentState = stateList[activeState];
                    currentStage = Stage.Enter;
                    running = true;
                    finished = false;
                    ran = true;
                }
            }
            if (finished || currentState == null)
                return;

            StateAction returnAction = currentState.Update(gameTime, currentStage);

            if (returnAction == StateAction.Continue)
            {
                switch (currentStage)
                {
                    case Stage.Enter:
                        currentStage = Stage.During;
                        break;
                    case Stage.During:
                        currentStage = Stage.Exit;
                        break;
                    case Stage.Exit:
                        {
                            if (activeState != -1)
                            {
                                if (currentState == stateList[activeState])
                                {
                                    activeState++;
                                    if (activeState >= stateList.Count)
                                        activeState = -1;
                                }
                            }

                            currentStage = Stage.Enter;
                            State stateOverride = currentState.OverrideGetNext();
                            if (stateOverride != null)
                                currentState = stateOverride;
                            else
                            {
                                if (activeState == -1)
                                    currentState = null;
                                else
                                    currentState = stateList[activeState];
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public enum StateAction
        {
            Remain,
            Continue,
        }

        public enum Stage
        {
            Enter,
            During,
            Exit
        }
    }
}
