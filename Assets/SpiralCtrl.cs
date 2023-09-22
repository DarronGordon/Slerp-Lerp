using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiralCtrl : MonoBehaviour
{
    Color color;
    [SerializeField] GameObject spherePrefab;
    [SerializeField] float amountOfVectors;
    [SerializeField] float radius;

    [SerializeField] List<GameObject> sphereList = new List<GameObject>();
    [SerializeField] List<Vector3> horizontalVectorList = new List<Vector3>();
    [SerializeField] List<Vector3> verticalVectorList = new List<Vector3>();

    [SerializeField] bool startSpiral;
    [SerializeField] float spiralSpaceDiff;
    [SerializeField] float spiralSpeed;

    float dir;
    private void Start()
    {
        StartCoroutine(CreateSpiral());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Flattern());
        }
    }

    IEnumerator CreateSpiral()
    {
        for (int i = 0; i < amountOfVectors; i++)
        {
            //Get point around circle spiral circum
            //r = 2Pi
            var radians = 2 * Mathf.PI / amountOfVectors * i;

            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            //Create vector pos dir
            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            // set position for vector to spawn
            var spawnPos = spawnDir * radius;

            //ADD Position to vector list for later use
            horizontalVectorList.Add(spawnPos);

            //instantiate vector
            GameObject transformVectorPos = Instantiate(spherePrefab, spawnPos, Quaternion.identity);

            //transform vector y pos for spiral pattern
            transformVectorPos.transform.Translate(new Vector3(0, transform.position.y + spiralSpaceDiff * i, 0));
            //add to list
            var t = new Vector3(0, transform.position.y + spiralSpaceDiff * i, 0);
            verticalVectorList.Add(t);

            sphereList.Add(transformVectorPos);


            yield return new WaitForSeconds(spiralSpeed);
        }
        yield return StartCoroutine(Flattern());
        yield return StartCoroutine(SingleOut());
        yield return StartCoroutine(StretchOut());
        yield return StartCoroutine(WrapAround());
        // yield return StartCoroutine(MoveToSinglePoint());
        yield return StartCoroutine(SFlattern());
        yield return StartCoroutine(SSingleOut());
        yield return StartCoroutine(SStretchOut());
        yield return StartCoroutine(SWrapAround());
    }

    IEnumerator Flattern()
    {

        foreach (GameObject sphere in sphereList)
        {
            Vector3 posToMoveTowards = new Vector3(sphere.transform.position.x, 0, sphere.transform.position.z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Lerp(sphere.transform.position, posToMoveTowards, step);
                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator SingleOut()
    {
        int ind = 0;
        foreach (GameObject sphere in sphereList)
        {
            ind++;
            if (ind >= 4)
            {
                dir = -1;
            }
            else if (ind < 4)
            {
                dir = 1;
            }

            Vector3 posToMoveTowards = new Vector3(sphereList[0].transform.position.x, 0, sphereList[0].transform.position.z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Lerp(sphere.transform.position, posToMoveTowards, step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator StretchOut()
    {
        int ind = 0;
        foreach (GameObject sphere in sphereList)
        {
            ind++;

            Vector3 posToMoveTowards = new Vector3(horizontalVectorList[0].x, horizontalVectorList[0].y + spiralSpaceDiff * ind, horizontalVectorList[0].z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Lerp(sphere.transform.position, posToMoveTowards, step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator WrapAround()
    {
        int ind = 0;
        foreach (GameObject sphere in sphereList)
        {
            ind++;

            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Lerp(sphere.transform.position, horizontalVectorList[ind-1] + verticalVectorList[ind -1], step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }


    }

    IEnumerator MoveToSinglePoint()
    {
        foreach (GameObject sphere in sphereList)
        {
            Vector3 posToMoveTowards = new Vector3(sphereList[0].transform.position.x, 0, sphereList[0].transform.position.z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Lerp(sphere.transform.position, posToMoveTowards, step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
       
        }


    // [[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[ SLERP CODE ]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]

    IEnumerator SFlattern()
    {

        foreach (GameObject sphere in sphereList)
        {
            Vector3 posToMoveTowards = new Vector3(sphere.transform.position.x, 0, sphere.transform.position.z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Slerp(sphere.transform.position, posToMoveTowards, step);
                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator SSingleOut()
    {
        int ind = 0;
        foreach (GameObject sphere in sphereList)
        {
            ind++;
            if (ind >= 4)
            {
                dir = -1;
            }
            else if (ind < 4)
            {
                dir = 1;
            }

            Vector3 posToMoveTowards = new Vector3(sphereList[0].transform.position.x, 0, sphereList[0].transform.position.z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Slerp(sphere.transform.position, posToMoveTowards, step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator SStretchOut()
    {
        int ind = 0;
        foreach (GameObject sphere in sphereList)
        {
            ind++;

            Vector3 posToMoveTowards = new Vector3(horizontalVectorList[0].x, horizontalVectorList[0].y + spiralSpaceDiff * ind, horizontalVectorList[0].z);
            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Slerp(sphere.transform.position, posToMoveTowards, step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }
    }
    IEnumerator SWrapAround()
    {
        int ind = 0;
        foreach (GameObject sphere in sphereList)
        {
            ind++;

            float step = 0f;

            while (step < 1)
            {
                step += Time.deltaTime;

                Vector3 pos = Vector3.Slerp(sphere.transform.position, horizontalVectorList[ind - 1] + verticalVectorList[ind - 1], step);

                sphere.transform.position = pos;

                yield return new WaitForEndOfFrame();
            }
        }


    }


}
