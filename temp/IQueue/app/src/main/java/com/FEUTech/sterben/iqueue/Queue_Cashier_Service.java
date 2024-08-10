//package com.FEUTech.sterben.iqueue;
//
//import android.annotation.SuppressLint;
//import android.app.Service;
//import android.content.Intent;
//import android.os.IBinder;
//import android.util.Log;
//
//@SuppressLint("Registered")
//public class Queue_Cashier_Service extends Service {
//
//    private static final String TAG = "HelloService";
//    //String specifiedqueue1;
//
//    @Override
//    public void onCreate() {
//
//    }
//
//    @Override
//    public int onStartCommand(Intent intent, int flags, int startId) {
//
//        if (intent != null && intent.getExtras() != null) {
//            //specifiedqueue1 = String.valueOf(intent.getIntExtra("specifiedQueue", 0));
//        }
//        new Thread(new Runnable() {
//            @Override
//            public void run() {
//
///*
//                new Handler().postDelayed(new Runnable() {
//                    @Override
//                    public void run() {
//                        FirebaseDatabase firebaseDatabase = FirebaseDatabase.getInstance();
//
//                        final DatabaseReference databaseReferenceMyCurrentQueue = firebaseDatabase
//                                .getReference("Cashier Transactions");
//
//                        databaseReferenceMyCurrentQueue.orderByChild("myCurrentQueue").limitToFirst(1)
//                                .addValueEventListener(new ValueEventListener() {
//                                    @Override
//                                    public void onDataChange(DataSnapshot dataSnapshot) {
//                                        //String queuenumber = String.valueOf(dataSnapshot.getValue());
//                                        //currentqueue1.setText(queuenumber);
//                                        for (DataSnapshot ds : dataSnapshot.getChildren()) {
//                                            String id = ds.child("myCurrentQueue").getValue(String.class);
//                                            Log.d("tag", id);
//                                            currentqueue1 = id;
//                                            //yung id yun ung sa current queue
//                                            //String queuenumber = String.valueOf(dataSnapshot.getValue());
//                                        }
//                                    }
//
//                                    @Override
//                                    public void onCancelled(DatabaseError databaseError) {
//
//                                    }
//                                });
//                    }
//                }, 1);*/
///*
//                consistentChecker = true;
//                runnable2 = new Runnable() {
//                    @Override
//                    public void run() {
//                        if (specifiedqueue1.equals(currentqueue1.trim())) {
//                            notificationAlert();
//                            consistentChecker = false;
//                            Toast.makeText(Queue_Cashier_Service.this, "servicestop", Toast.LENGTH_SHORT).show();
//                            cancelflags();
//                            stopSelf();
//                        }
//                        if (consistentChecker) {
//                            Toast.makeText(Queue_Cashier_Service.this, "serviceloop", Toast.LENGTH_SHORT).show();
//                            handler2.postDelayed(runnable2, 2000);
//                        }
//                    }
//                };
//                handler2.postDelayed(runnable2, 2000);
//            }
//
//            //Stop service once it finishes its task
//            //stopSelf();
//        }).start();
//        */
//
//            }
//        });
//        return Service.START_STICKY;
//
//    }
//
//    @Override
//    public IBinder onBind(Intent arg0) {
//        return null;
//    }
//
////
////    private void notificationAlert() {
////        Intent intent = new Intent(this, Login.class);
////        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
////        PendingIntent pendingIntent = PendingIntent.getActivity(this, 1410,
////                intent, PendingIntent.FLAG_ONE_SHOT);
////        NotificationCompat.Builder notificationBuilder = new
////                NotificationCompat.Builder(this)
////                .setSmallIcon(R.drawable.feutech_iqueue)
////                .setContentTitle("IQueue")
////                .setContentText("Proceed to counter")
////                .setAutoCancel(true)
////                .setVibrate(new long[]{1000, 1000, 1000})
////                .setColor(ContextCompat.getColor(getApplicationContext(), R.color.colorPrimary))
////                .setDefaults(Notification.DEFAULT_ALL)
////                .setPriority(Notification.PRIORITY_MAX)
////                .setContentIntent(pendingIntent);
////
////
////        PowerManager pm = (PowerManager) getSystemService(Context.POWER_SERVICE);
////        PowerManager.WakeLock wl = pm.newWakeLock(PowerManager.PARTIAL_WAKE_LOCK | PowerManager.ACQUIRE_CAUSES_WAKEUP, "TAG");
////        wl.acquire(10000);
////
////        NotificationManager notificationManager =
////                (NotificationManager)
////                        getSystemService(Context.NOTIFICATION_SERVICE);
////
////        notificationManager.notify(1410, notificationBuilder.build());
////    }
////
////    private void cancelflags() {
////        handler2.removeCallbacks(runnable2);
////    }
//
//    @Override
//    public void onDestroy() {
//
//    }
//}