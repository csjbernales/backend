package com.FEUTech.sterben.iqueue;

/**
 * Created by Sterben on 1/11/2018.
 */

public class Queue_FirebaseAdapterCCS {
    private String dataKey;
    private String MyQueue;
    private String FullName;
    private String studnum;
    private String Purpose;
    private String call;
    private String counter;
    private String transID;
    private String queueStatus;

    public Queue_FirebaseAdapterCCS() {
        //this constructor is required
    }

    public Queue_FirebaseAdapterCCS(String dataKey, String myCurrentQueue, String fullname, String studnum,
                                    String purp, String call, String counter, String transID, String queueStatus) {
        this.dataKey = dataKey;
        this.MyQueue = myCurrentQueue;
        this.FullName = fullname;
        this.Purpose = purp;
        this.studnum = studnum;
        this.call = call;
        this.counter = counter;
        this.transID = transID;
        this.queueStatus = queueStatus;
    }

    public String getId() {
        return dataKey;
    }

    public String getStudnum() {
        return studnum;
    }

    public String getmyCurrentQueue() {
        return MyQueue;
    }

    public String getFullName() {
        return FullName;
    }

    public String getPurpose() {
        return Purpose;
    }

    public String getCall() {
        return call;
    }

    public String getCounter() {
        return counter;
    }

    public String getTransID() {
        return transID;
    }

    public String getQueueStatus() {
        return queueStatus;
    }
}
