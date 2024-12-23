package com.maxholmes.androidapp.screen.test

import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.databinding.ActivityTestBinding

class TestActivity : AppCompatActivity() {

    private lateinit var binding: ActivityTestBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityTestBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val token = intent.getStringExtra("TOKEN")
        val courtId = intent.getStringExtra("courtId")
        val selectedDay = intent.getStringExtra("selectedDay", )

        binding.tokenTextView.text = "Token: $token"
        binding.test1.text = "Court ID: $courtId"
        binding.test2.text = "Selected Day: $selectedDay"
    }
}
