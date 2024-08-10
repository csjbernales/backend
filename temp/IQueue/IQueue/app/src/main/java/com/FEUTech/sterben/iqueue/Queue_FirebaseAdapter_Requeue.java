package com.FEUTech.sterben.iqueue;

/**
 * Created by Sterben on 1/11/2018.
 */

public class Queue_FirebaseAdapter_Requeue {
    private String dataKey;
    private String queueStatus;

    public Queue_FirebaseAdapter_Requeue() {
        //this constructor is required
    }

    public Queue_FirebaseAdapter_Requeue(String dataKey, String queueStatus) {
        this.dataKey = dataKey;
        this.queueStatus = queueStatus;
    }

    public String getId() {
        return dataKey;
    }

    public String getQueueStatus() {
        return queueStatus;
    }
}
