package com.FEUTech.sterben.iqueue;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;

public class Pane_My_Queue_Redirector extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Intent in2 = getIntent();
        Bundle b2 = in2.getExtras();
        String s2 = b2.getString("num");
        String s3 = b2.getString("itemPos");

        switch (s3) {
            case "0":
                Bundle bundle = new Bundle();
                bundle.putString("num", s2);
                Intent intent = new Intent(Pane_My_Queue_Redirector.this, Pane_Cashier.class);
                intent.putExtras(bundle);
                startActivity(intent);
                overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                finish();
                break;
            case "1":
                Bundle bundle2 = new Bundle();
                bundle2.putString("num", s2);
                Intent intent2 = new Intent(Pane_My_Queue_Redirector.this, Pane_Registrar.class);
                intent2.putExtras(bundle2);
                startActivity(intent2);
                overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                finish();
                break;
            case "2":
                Bundle bundle3 = new Bundle();
                bundle3.putString("num", s2);
                Intent intent3 = new Intent(Pane_My_Queue_Redirector.this, Pane_CCS.class);
                intent3.putExtras(bundle3);
                startActivity(intent3);
                overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                finish();
                break;
            case "3":
                Bundle bundle4 = new Bundle();
                bundle4.putString("num", s2);
                Intent intent4 = new Intent(Pane_My_Queue_Redirector.this, Pane_Accounting.class);
                intent4.putExtras(bundle4);
                startActivity(intent4);
                overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                finish();
                break;
            default:
                break;
        }
    }
}
