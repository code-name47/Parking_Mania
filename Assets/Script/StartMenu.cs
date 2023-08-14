using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class StartMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        UserDataHandler m_userDataHandler;
        void Start()
        {
            //singleton calling
            m_userDataHandler = UserDataHandler.Instance();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}