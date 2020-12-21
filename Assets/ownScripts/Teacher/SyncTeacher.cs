using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTeacher : MonoBehaviour
{
    [Serializable]
    public class TeacherSync : JsonData
    {
        public float x, z, facingX, facingZ;

        public TeacherSync(Transform t)
        {
            this.x = t.position.x;
            this.z = t.position.z;

            this.facingX = this.x + t.forward.x;
            this.facingZ = this.z + t.forward.z;
        }
    }

    public float SyncFrequency = 10f; // 10 FPS
    
    private float current = 0f;

    private void Update()
    {
        current += Time.deltaTime;

        float fraction = 1 / SyncFrequency;
        if (current > fraction) // TODO: Check if server is even up
        {
            NetworkController.Handler.Respond("syncTeacher", new TeacherSync(this.transform));
            current -= fraction;
        }
    }
}
