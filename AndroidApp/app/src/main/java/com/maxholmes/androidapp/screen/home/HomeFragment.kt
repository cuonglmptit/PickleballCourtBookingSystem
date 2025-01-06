package com.maxholmes.androidapp.screen.home

import android.os.Bundle
import android.view.LayoutInflater
import android.widget.Toast
import androidx.recyclerview.widget.GridLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.repository.CourtClusterRepository
import com.maxholmes.androidapp.databinding.FragmentHomeBinding
import com.maxholmes.androidapp.screen.detail.courtcluster.CourtClusterDetailFragment
import com.maxholmes.androidapp.screen.home.adapter.CourtClusterAdapter
import com.maxholmes.androidapp.screen.menu.MenuFragment
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
        onNavigationItemSelected()
    }

    override fun initData() {
        mHomePresenter =
            HomePresenter(
                mCourtClusterRepository = CourtClusterRepository()
            )
        mHomePresenter.setView(this)

        val fakeCourtClusters = mutableListOf<CourtCluster>()
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
                    putParcelable("COURT_CLUSTER", item)
                }
            val courtClusterDetailFragment: CourtClusterDetailFragment = CourtClusterDetailFragment()
            courtClusterDetailFragment.arguments = bundle
            parentFragmentManager.beginTransaction()
                .replace(R.id.layoutContainer, courtClusterDetailFragment)
                .addToBackStack(null)
                .commit()
        } else {
            Toast.makeText(requireContext(), "Selected item is null", Toast.LENGTH_SHORT).show()
        }
    }

    private fun onNavigationItemSelected() {
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

                R.id.search -> {

                    true
                }

                R.id.user -> {
                    Toast.makeText(context, "Settings", Toast.LENGTH_SHORT).show()
                    parentFragmentManager.beginTransaction()
                        .replace(R.id.layoutContainer, MenuFragment())
                        .addToBackStack(null)
                        .commit()
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