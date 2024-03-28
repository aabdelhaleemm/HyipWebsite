import {Component, OnInit} from '@angular/core';
import {PlansService} from "../../_services/plans.service";
import {PlansModel} from "../../_models/plansModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";
import {AdminService} from "../../_services/admin.service";
import {StateModel} from "../../_models/stateModel";

@Component({
  selector: 'app-admin-plans',
  templateUrl: './admin-plans.component.html',
  styleUrls: ['./admin-plans.component.css']
})
export class AdminPlansComponent implements OnInit {
  plans?: PlansModel[];
  editPlanForm = new FormGroup({
    id: new FormControl({value: '', disabled: true}, []),
    name: new FormControl({value: '', disabled: true}, []),
    max: new FormControl({value: '', disabled: true}, []),
    min: new FormControl({value: '', disabled: true}, []),
    current: new FormControl('', [Validators.required])
  })
  isDone = false;
  state?: StateModel;

  constructor(private plansService: PlansService, private toast: ToastrService, private adminService: AdminService) {
  }

  ngOnInit(): void {
    this.getPlans()
  }

  getState() {
    this.adminService.getState().subscribe(res => {
      if (res) {
        this.state = res;
      }
    })
  }

  getPlans() {
    this.plansService.getPlans().subscribe(res => {
      if (res) {
        this.plans = res;
      }
    })
  }

  changePercent() {
    this.plansService.changeProfitPercent({
      'PlanId': this.editPlanForm.get('id')?.value,
      'NewProfitPercent': this.editPlanForm.get('current')?.value
    }).subscribe(res => {
      if (res) {
        this.toast.success("Updated Successfully ")
        this.getPlans()
        this.isDone = true;
      }
    })
  }

  handleChangeClick(id: number, name: string, max: number, min: number, current: number) {
    this.isDone = false;
    this.editPlanForm.reset()
    this.editPlanForm.setValue({id: id, name: name, max: max, min: min, current: current})
  }

  get max() {
    return this.editPlanForm.get('max')?.value;
  }

  get min() {
    return this.editPlanForm.get('min')?.value;
  }

  get current() {
    return this.editPlanForm.get('min')?.value;
  }

  isValid() {
    return this.editPlanForm.get('current')?.value <= this.max && this.editPlanForm.get('current')?.value >= this.min;
  }
}
