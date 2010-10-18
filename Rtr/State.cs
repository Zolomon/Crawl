using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lampa
{
    public class State
    {
        virtual public StateMachine.StateAction Enter(GameTime gameTime)
        {
            return StateMachine.StateAction.Continue;
        }
        virtual public StateMachine.StateAction During(GameTime gameTime)
        {
            return StateMachine.StateAction.Continue;
        }
        virtual public StateMachine.StateAction Exit(GameTime gameTime)
        {
            return StateMachine.StateAction.Continue;
        }

        virtual public void Draw(StateMachine.Stage stage)
        {

        }

        virtual public State OverrideGetNext()
        {
            return null;
        }

        virtual public State Reset()
        {
            return this;
        }

        public StateMachine.StateAction Update(GameTime gameTime, StateMachine.Stage stage)
        {
            switch (stage)
            {
                case StateMachine.Stage.Enter:
                    return Enter(gameTime);
                case StateMachine.Stage.During:
                    return During(gameTime);
                case StateMachine.Stage.Exit:
                    return Exit(gameTime);
                default:
                    return StateMachine.StateAction.Continue;
            }
        }


    }
}
