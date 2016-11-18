using UnityEngine;
using System.Collections;

public class UnityChanController : MonoBehaviour {

	// Use this for initialization
   
        //アニメーションするためのコンポーネントを入れる
        private Animator myAnimator;
    //アニメの状態の情報を入れる
    private AnimatorStateInfo animState;
    //unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前進するための力
    private float forwardForce = 800.0f;

    private float turnForce = 500.0f;

    private float upForce = 500.0f;

    private float movableRange = 3.4f;

    private float coefficient = 0.95f;

    private bool isEnd = false;

	void Start () {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);
        //Rigidbodyコンポーネントを取得   
        this.myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        //unityちゃんに前方向の力を加える
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        if((Input.GetKey(KeyCode.A)) && -this.movableRange < this.transform.position.x)
        {
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }else if((Input.GetKey(KeyCode.D)) && this.transform.position.x < this.movableRange)
        {
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);

            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
	
	}
    void OntriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
        }

        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
        }
    }
}
