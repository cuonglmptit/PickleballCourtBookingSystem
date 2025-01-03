package com.maxholmes.androidapp.screen.customer.booking

import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.databinding.ActivityBookingBinding
import com.maxholmes.androidapp.screen.customer.booking.adapter.CourtTimeSlotAdapter
import com.maxholmes.androidapp.screen.customer.booking.adapter.SelectCourtAdapter
import com.maxholmes.androidapp.screen.customer.booking.adapter.SelectDayAdapter
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import java.time.LocalTime
import java.time.format.DateTimeFormatter
import java.util.Calendar

class BookingActivity : AppCompatActivity() {
    private lateinit var binding: ActivityBookingBinding
    private lateinit var selectCourtAdapter: SelectCourtAdapter
    private lateinit var selectDayAdapter: SelectDayAdapter
    lateinit var courtTimeSlotAdapter: CourtTimeSlotAdapter

    private var selectedDay: Calendar? = null
    private var selectedCourt: Court? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBookingBinding.inflate(layoutInflater)
        setContentView(binding.root)

        selectCourtAdapter = SelectCourtAdapter()
        selectDayAdapter = SelectDayAdapter()
        courtTimeSlotAdapter = CourtTimeSlotAdapter()

        selectedDay = intent.getSerializableExtra("selectedDay", Calendar::class.java)
        selectedCourt = intent.getParcelableExtra("court", Court::class.java)

        updateSelectedDayAndCourt()

        binding.recyclerViewCourt.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewCourt.adapter = selectCourtAdapter

        binding.recyclerViewDay.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewDay.adapter = selectDayAdapter

        binding.recyclerViewTimeSlot.layoutManager = GridLayoutManager(this, 3, GridLayoutManager.VERTICAL, false)
        binding.recyclerViewTimeSlot.adapter = courtTimeSlotAdapter

        loadCourts()
        loadDays()
        loadTimeSlots()

        selectDayAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Calendar> {
            override fun onItemClick(item: Calendar?) {
                item?.let {
                    val position = selectDayAdapter.getSelectDays().indexOf(it)
                    selectDayAdapter.setSelectedItem(position)
                    selectedDay = it
                    updateSelectedDayAndCourt()
                }
            }
        })

        selectCourtAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Court> {
            override fun onItemClick(item: Court?) {
                item?.let {
                    val position = selectCourtAdapter.getCourts().indexOf(it)
                    selectCourtAdapter.setSelectedItem(position)
                    selectedCourt = it
                    updateSelectedDayAndCourt()
                }
            }
        })

        courtTimeSlotAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtTimeSlot> {
            override fun onItemClick(item: CourtTimeSlot?) {
                item?.let {
                    val position = courtTimeSlotAdapter.getCourtTimeSlots().indexOf(it)
                    if (position != RecyclerView.NO_POSITION) {
                        courtTimeSlotAdapter.toggleSelection(position)
                    }
                }
            }
        })
    }



    private fun updateSelectedDayAndCourt() {
        selectedDay?.let {
            val position = selectDayAdapter.getSelectDays().indexOf(it)
            selectDayAdapter.setSelectedItem(position)
        }

        selectedCourt?.let {
            val position = selectCourtAdapter.getCourts().indexOf(it)
            selectCourtAdapter.setSelectedItem(position)
        }
    }

    private fun loadTimeSlots() {
//        courtTimeSlotAdapter.updateData(timeSlots)
    }

    private fun loadDays() {
        val calendar = Calendar.getInstance()
        val selectDays = mutableListOf<Calendar>()

        for (i in 0..6) {
            val day = calendar.clone() as Calendar
            day.add(Calendar.DAY_OF_YEAR, i)
            selectDays.add(day)
        }

        selectDayAdapter.updateData(selectDays)
    }

    private fun loadCourts() {
        val courts = intent.getParcelableArrayListExtra("courts", Court::class.java)
        selectCourtAdapter.updateData(courts)
    }
}
