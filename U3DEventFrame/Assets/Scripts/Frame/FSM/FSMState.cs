using UnityEngine;
using System.Collections;


namespace U3DEventFrame
{


	public abstract class FSMState
	{
		
		public byte StateId;
		
		
		public virtual void OnBeforEnter()
		{}
		public virtual void CopyState(FSMState frontState)
		{}
		public abstract void OnEnter();
		
		public virtual void Update()
		{}
		public virtual void OnLeave()
		{} 
		
		
		
		
	}



	public class FSMManager
	{
		
		private FSMState[]   fsmManager ;
		
		private byte curAdd;
		
		
		private short curStateId;

        public short CurStateId
        {
            get
            {
                return curStateId;
            }
            set
            {
                curStateId = value;
            }
        }
		public FSMManager(int number)
		{
			
			curAdd = 0;
			curStateId = -1;
			fsmManager = new FSMState[number];
		}
		
		
		public  void  AddState( FSMState  tmpState)
		{
			if (curAdd < fsmManager.Length )
			{
				tmpState.StateId = curAdd ;
				fsmManager[curAdd] = tmpState;
				
				curAdd++;
			}
		}
		
		public FSMState GetCurrtState()
		{
			
			return fsmManager[curStateId];
		}
		public void ChangerState(byte stateId)
		{
			
			if(stateId != curStateId)
			{

                if (curStateId != -1)
                {
                    fsmManager[curStateId].OnLeave();

                    fsmManager[curStateId].CopyState(fsmManager[stateId]);
                }
				
	
				fsmManager[stateId].OnBeforEnter();
				
				curStateId = stateId;
				
				fsmManager[stateId].OnEnter();
			}
			
			
		}
		
		public void Update()
		{

            if (curStateId !=-1)
			fsmManager[curStateId].Update();
		}
		
		
		
		
		
	}

}



