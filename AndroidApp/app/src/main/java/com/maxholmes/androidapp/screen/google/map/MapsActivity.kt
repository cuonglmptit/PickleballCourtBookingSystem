package com.maxholmes.androidapp.screen.google.map

import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageView
import com.bumptech.glide.Glide

import com.google.android.gms.maps.CameraUpdateFactory
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.OnMapReadyCallback
import com.google.android.gms.maps.SupportMapFragment
import com.google.android.gms.maps.model.LatLng
import com.google.android.gms.maps.model.MarkerOptions
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Address
import com.maxholmes.androidapp.databinding.ActivityMapsBinding

class MapsActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_maps)

        val address = Address(
            id = "1",
            city = "Hanoi",
            district = "Cau Giay",
            ward = "Dich Vong Hau",
            street = "Nguyen Phong Sac"
        )

        val addressQuery = "${address.street}, ${address.ward}, ${address.district}, ${address.city}"
        val mapUri = "http://maps.google.com/maps?q=$addressQuery"

        val intent = Intent(Intent.ACTION_VIEW, Uri.parse(mapUri))
        intent.putExtra(Intent.EXTRA_REFERRER, Uri.parse("android-app://com.google.android.apps.maps"))
        startActivity(intent)
    }
}