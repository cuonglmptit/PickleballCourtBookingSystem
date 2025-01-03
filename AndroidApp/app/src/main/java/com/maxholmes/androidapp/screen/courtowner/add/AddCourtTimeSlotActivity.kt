package com.maxholmes.androidapp.screen.courtowner.add

import DateAdapter
import PriceAdapter
import android.os.Bundle
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtPrice
import java.time.LocalTime
import kotlin.random.Random
import java.util.UUID

class AddCourtTimeSlotActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_court_time_slot)

        val dateRecyclerView: RecyclerView = findViewById(R.id.dateRecyclerView)
        val priceRecyclerView: RecyclerView = findViewById(R.id.priceRecyclerView)

        // Generate fake data
        val fakeDates = generateFakeDates()
        val fakePrices = generateFakePrices()

        // Set up the RecyclerViews with adapters
        dateRecyclerView.layoutManager = LinearLayoutManager(this)
        priceRecyclerView.layoutManager = LinearLayoutManager(this)

        // Set up the adapters (make sure you have an adapter ready for both)
        dateRecyclerView.adapter = DateAdapter(fakeDates)  // Custom adapter for date list
        priceRecyclerView.adapter = PriceAdapter(fakePrices)  // Custom adapter for price list
    }

    // Generate fake dates in dd/MM/yyyy format
    private fun generateFakeDates(): List<String> {
        val dateList = mutableListOf<String>()
        val currentDate = java.time.LocalDate.now()
        val formatter = java.time.format.DateTimeFormatter.ofPattern("dd/MM/yyyy")

        // Generate 10 random dates starting from today
        for (i in 0 until 10) {
            val randomDate = currentDate.plusDays(i.toLong())
            dateList.add(randomDate.format(formatter))
        }

        return dateList
    }

    // Generate fake prices for different time slots
    private fun generateFakePrices(): List<CourtPrice> {
        val priceList = mutableListOf<CourtPrice>()
        val timeSlots = listOf(
            LocalTime.of(8, 0), // 8:00 AM
            LocalTime.of(10, 0), // 10:00 AM
            LocalTime.of(12, 0), // 12:00 PM
            LocalTime.of(14, 0), // 2:00 PM
            LocalTime.of(16, 0)  // 4:00 PM
        )

        // Generate random prices for each time slot
        for (time in timeSlots) {
            val random = Random.nextInt(0, 2)
            var price: Double
            if(random == 0)
                price = 100000.0
            else
                price = 150000.0
            priceList.add(CourtPrice(
                id = UUID.randomUUID().toString(),
                time = time,
                price = price,
                courtClusterId = "cluster_01"
            ))
        }

        return priceList
    }
}
