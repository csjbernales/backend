package com.FEUTech.sterben.iqueue;

import retrofit.Callback;
import retrofit.client.Response;
import retrofit.http.Field;
import retrofit.http.FormUrlEncoded;
import retrofit.http.POST;

public interface Queue_Registrar_Send_Data {
    @FormUrlEncoded
    @POST("/steb/addToRegistrar.php")
    public void insertUserRegistrar(

            @Field("id") String id,
            @Field("fireID") String fireID,
            @Field("id123") String id123,
            @Field("lcourse") String lcourse,
            @Field("firstname") String firstname,
            @Field("middlename") String middlename,
            @Field("lastname") String lastname,
            @Field("top") String top,
            @Field("gcurrent_sy") String gcurrent_sy,
            @Field("gcurrent_term") String gcurrent_term,
            @Field("contactnum") String contactnum,
            Callback<Response> callback);
}
