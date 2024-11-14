package com.maxholmes.androidapp.screen.home

import android.os.Bundle
import android.view.LayoutInflater
import android.widget.Toast
import androidx.recyclerview.widget.GridLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.repository.CourtClusterRepository
import com.maxholmes.androidapp.databinding.FragmentHomeBinding
import com.maxholmes.androidapp.screen.detail.court.CourtDetailFragment
import com.maxholmes.androidapp.screen.home.adapter.CourtClusterAdapter
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.base.BaseFragment
import java.time.LocalTime

class HomeFragment :
    BaseFragment<FragmentHomeBinding>(),
    HomeContract.View,
    OnItemRecyclerViewClickListener<CourtCluster> {
    private lateinit var mHomePresenter: HomePresenter
    private val mCourtClusterAdapter: CourtClusterAdapter by lazy { CourtClusterAdapter() }

    override fun inflateViewBinding(inflater: LayoutInflater): FragmentHomeBinding {
        return FragmentHomeBinding.inflate(inflater)
    }

    override fun initView() {
        viewBinding.recyclerViewCourtCluster.apply {
            layoutManager = GridLayoutManager(context, 1)
            adapter = mCourtClusterAdapter
        }
        mCourtClusterAdapter.registerItemRecyclerViewClickListener(this)
        setupBottomNavigation()
    }

    override fun initData() {
        mHomePresenter =
            HomePresenter(
                mCourtClusterRepository = CourtClusterRepository()
            )
        mHomePresenter.setView(this)

        val fakeCourtClusters = mutableListOf(
            CourtCluster(
                id = "1",
                name = "Sân Pickleball Hoàn Kiếm",
                description = "Sân chơi Pickleball tại trung tâm Hà Nội, khu vực Hoàn Kiếm.",
                openingTime = LocalTime.of(5, 0),
                closingTime = LocalTime.of(22, 0),
                addressId = "Hoàn Kiếm, Hà Nội",
                courtOwnerId = "d1e0c764-fbb9-47ab-836d-0645f94ffb80",
                imageUrl = "https://picsum.photos/200"
            ),
            CourtCluster(
                id = "2",
                name = "Sân Pickleball Ba Đình",
                description = "Sân chơi Pickleball ở quận Ba Đình, gần các khu vực công sở.",
                openingTime = LocalTime.of(5, 0),
                closingTime = LocalTime.of(22, 0),
                addressId = "Ba Đình, Hà Nội",
                courtOwnerId = "f7a7f935-22b4-4db1-b7e3-0fa240c1dbf6",
                imageUrl = "https://picsum.photos/200"
            ),
            CourtCluster(
                id = "3",
                name = "Sân Pickleball Tây Hồ",
                description = "Sân chơi Pickleball nằm tại quận Tây Hồ, gần Hồ Tây.",
                openingTime = LocalTime.of(5, 0),
                closingTime = LocalTime.of(22, 0),
                addressId = "Tây Hồ, Hà Nội",
                courtOwnerId = "4c8f233d-d741-49d4-9d8b-ea91a6c4b408",
                imageUrl = "https://picsum.photos/200"
            ),
            CourtCluster(
                id = "4",
                name = "Sân Pickleball Cầu Giấy",
                description = "Sân chơi Pickleball tại quận Cầu Giấy, khu vực gần các trung tâm thể thao.",
                openingTime = LocalTime.of(5, 0),
                closingTime = LocalTime.of(22, 0),
                addressId = "Cầu Giấy, Hà Nội",
                courtOwnerId = "3a29c019-e85b-48ec-b30d-4097bcba8157",
                imageUrl = "https://picsum.photos/200"
            ),
            CourtCluster(
                id = "5",
                name = "Sân Pickleball Long Biên",
                description = "Sân chơi Pickleball tại quận Long Biên, không gian rộng rãi, thoáng mát.",
                openingTime = LocalTime.of(5, 0),
                closingTime = LocalTime.of(22, 0),
                addressId = "Long Biên, Hà Nội",
                courtOwnerId = "a72b0891-25c3-4e93-8f8f-e2f6002052fc",
                imageUrl = "https://picsum.photos/200"
            ),
            CourtCluster(
                id = "6",
                name = "Sân Pickleball Thanh Xuân",
                description = "Sân Pickleball hiện đại tại quận Thanh Xuân, dễ dàng di chuyển từ các khu vực trung tâm.",
                openingTime = LocalTime.of(5, 0),
                closingTime = LocalTime.of(22, 0),
                addressId = "Thanh Xuân, Hà Nội",
                courtOwnerId = "d9577f56-c4a9-493b-87b5-b2b45511e462",
                imageUrl = "https://picsum.photos/200"
            )
        )
        mCourtClusterAdapter.updateData(fakeCourtClusters)
    }

//    override fun onGetCourtSuccess(courts: MutableList<Court>) {
//        mCourtAdapter.updateData(courts)
//    }

    override fun onError(exception: Exception?) {
        Toast.makeText(context, exception?.message, Toast.LENGTH_SHORT).show()
    }

    override fun onItemClick(item: CourtCluster?) {
        if (item != null) {
            val bundle =
                Bundle().apply {
                    putParcelable("COURT", item)
                }
//            parentFragmentManager.beginTransaction()
//                .replace(R.id.layoutContainer, CourtDetailFragment.newInstance(item))
//                .addToBackStack(null)
//                .commit()
        } else {
            Toast.makeText(requireContext(), "Selected item is null", Toast.LENGTH_SHORT).show()
        }
    }

    private fun setupBottomNavigation() {
        viewBinding.bottomNavigationView.setOnItemSelectedListener {
            when (it.itemId) {
                R.id.home -> {
                    Toast.makeText(context, "Home", Toast.LENGTH_SHORT).show()
                    true
                }

                R.id.booking -> {
                    Toast.makeText(context, "Booking", Toast.LENGTH_SHORT).show()
                    true
                }

                R.id.notification -> {
                    Toast.makeText(context, "Notification", Toast.LENGTH_SHORT).show()
                    true
                }

                R.id.user -> {
                    Toast.makeText(context, "Settings", Toast.LENGTH_SHORT).show()
                    true
                }

                else -> false
            }
        }
    }

    companion object {
        fun newInstance() = HomeFragment()
    }
}