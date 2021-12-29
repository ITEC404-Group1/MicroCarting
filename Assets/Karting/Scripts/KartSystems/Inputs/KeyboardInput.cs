using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        private GameFlowManager flowManager;
        void Start() => flowManager = FindObjectOfType<GameFlowManager>(); 
        public FixedJoystick movejoystick;
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";
        private float hoz;
        void Update()
    {
        hoz = movejoystick.Horizontal*.8f;
    }
        public override InputData GenerateInput() {
            return new InputData
            { 
                
                Accelerate = flowManager.acceleration ==1?true:false,
                Brake = flowManager.braking ==-1?true:false,
                TurnInput = hoz 
                
            };
        }
    }
}
