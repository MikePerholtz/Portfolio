
<template>
    <form v-on:submit.prevent="onSubmit">
        <div id="login_form_pfo">
            <div class="form-group">
                <label for="username_pfo">Username</label>
                <input id="username_pfo" class="form-control" type="text">
            </div>
            <div class="form-group">
                <label for="password_pfo"></label>
                <input id="password_pfo" class="form-control" type="password">
            </div>
            <input type="submit" class="btn btn-primary" value="Login" />
        </div>
    </form>
</template>

<script>
    export default {
        data() {
            return {
                creds: {},
                exception: "",
            }
        },
        methods: {
            onSubmit() {
            
            },
            waitForIt() {

            }
        }
    }
</script>

<style>

</style>