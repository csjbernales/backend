package com.FEUTech.sterben.iqueue;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.ImageView;
import android.widget.Toast;

import co.ceryle.fitgridview.FitGridAdapter;

class Adapter_queuePane extends FitGridAdapter {

    Bundle bundle2;

    private int[] drawables = {
            R.drawable.bit_cashier,
            R.drawable.bit_registrar,
            R.drawable.bit_ccs,
            R.drawable.bit_accounting};

    private Context context;

    Adapter_queuePane(Context context) {
        super(context, R.layout.grid_item);
        this.context = context;
    }

    @Override
    public void onBindView(final int position, View itemView) {
        ImageView iv = (ImageView) itemView.findViewById(R.id.grid_item_iv);
        iv.setImageResource(drawables[position]);
        Intent intent = ((AppCompatActivity) context).getIntent();
        Intent in2 = intent;
        Bundle b2 = in2.getExtras();
        String s2 = b2.getString("num");
        bundle2 = new Bundle();
        bundle2.putString("num", s2);

        itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                switch (position) {
                    case 0:
                        Intent intent2 = new Intent(context, Pane_My_Queue_Redirector.class);
                        String zero = "0";
                        bundle2.putString("itemPos", zero);
                        intent2.putExtras(bundle2);
                        context.startActivity(intent2, bundle2);
                        break;
                    case 1:
                        Intent intent3 = new Intent(context, Pane_My_Queue_Redirector.class);
                        String one = "1";
                        bundle2.putString("itemPos", one);
                        intent3.putExtras(bundle2);
                        context.startActivity(intent3, bundle2);
                        break;
                    case 2:
                        Intent intent4 = new Intent(context, Pane_My_Queue_Redirector.class);
                        String two = "2";
                        bundle2.putString("itemPos", two);
                        intent4.putExtras(bundle2);
                        context.startActivity(intent4, bundle2);
                        break;
                    case 3:
                        Intent intent5 = new Intent(context, Pane_My_Queue_Redirector.class);
                        String three = "3";
                        bundle2.putString("itemPos", three);
                        intent5.putExtras(bundle2);
                        context.startActivity(intent5, bundle2);
                        break;
                    default:
                        Toast.makeText(context, "", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
}
