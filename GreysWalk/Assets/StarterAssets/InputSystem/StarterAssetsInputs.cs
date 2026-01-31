using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Interaction Settings")]
		public bool interact = false;

		public bool isInDialog = false;
		public bool isUIMode = false;

		private Animator _animator;

		void Start()
		{
			_animator = GetComponentInChildren<Animator>();
		}

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			_animator.SetBool("isWalking", newMoveDirection != Vector2.zero);
			if(isInDialog)
			{
				move = Vector2.zero;
				return;
			}
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			if(isInDialog) {
				look = Vector2.zero;
				return;
			}
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			if(isInDialog)
			{
				jump = false;
				return;
			}
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			if(isInDialog) {
				sprint = false;
				return;
			}
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		private void InteractInput(bool newInteractState)
		{
			if(isInDialog && isUIMode)
			{
				interact = false;
				return;
			}
			interact = newInteractState;
		}

		public void SetUIMode(bool state)
		{
			isUIMode = state;
			cursorLocked = !state;
			SetCursorState(cursorLocked);
		}

	}
	
}