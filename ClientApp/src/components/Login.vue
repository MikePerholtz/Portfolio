
<template>
    <div id="login_pfo">
        <b-row align-v="center" v-bind:class="{ 'h-100': centerFormVertically }">
            <b-container class="w-50"> 
                <b-form v-on:submit.prevent="onSubmit">
                    <b-form-group
                        label-cols-sm="4"
                        label-cols-lg="3" 
                        label=""
                        label-for="">
                        <h3 class="text-left">Login</h3>
                    </b-form-group>
                    <b-form-group
                        label-cols-sm="4"
                        label-cols-lg="3" 
                        label="Username"
                        label-for="username_pfo">
                        <b-form-input id="username_pfo" name="username" type="text" v-model="creds.username"></b-form-input>
                    </b-form-group>
                    <b-form-group
                        label-cols-sm="4"
                        label-cols-lg="3" 
                        label="Password"
                        label-for="password_pfo">
                        <b-form-input id="password_pfo" name="password" type="password" v-model="creds.password"></b-form-input>
                    </b-form-group>

                    <b-form-group
                        label-cols-sm="4"
                        label-cols-lg="3"
                        label=""
                        label-for=""
                        class="text-left">
                        <b-button type="submit" pill variant="outline-primary">Submit</b-button>
                    </b-form-group>
                </b-form>
            </b-container>
        </b-row>
    </div>
</template>

<script>
    import theMixin from '../mixins/mixins'
    //import _ from "lodash";
   
    
    export default {
        mixins: [theMixin],
        // mixins: [theMixin(
        //     {
        //         centerFormVertically: true,
        //         elemToCenter: "#login_pfo"
        //     }
        //)],
        data() {
            return {
                centerFormVertically: true,
                elemToCenter: "#login_pfo",
                creds: {},
                exception: "",
            }
        },
        // props: 
        //     ['centerFormVertically']
        // ,
        
        methods: {
            onSubmit() {
                alert(this.creds.username + " "+this.creds.password);
            },
            waitForIt() {

            }
            
        }
    }
</script>

<style>
    /* body, html, #app {
        height:100%;
    } */
</style>