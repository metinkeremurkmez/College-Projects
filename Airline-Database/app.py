from flask import Flask, render_template, request, redirect, url_for, flash
from flask_mysqldb import MySQL

app = Flask(__name__)
app.config['MYSQL_HOST'] = 'localhost'
app.config['MYSQL_USER'] = 'root'
app.config['MYSQL_PASSWORD'] = 'K+332776:x'
app.config['MYSQL_DB'] = 'airline'

mysql = MySQL(app)

app.secret_key = "mysecretkey"

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/results', methods=['GET','POST'])
def results():
    if request.method == 'POST':
        cursor_ = mysql.connection.cursor()
        passport = request.form.get('passport_no')
        passport = int(passport)
        cursor_.execute(
            "SELECT ffp.Ffp_Status FROM ffp,member_of WHERE Passport_no = %s and member_of.Ffp_id = ffp.Ffp_id",
            (passport,))
        status = cursor_.fetchall()

        cursor_.execute(
            "SELECT ffp.Ffp_name FROM ffp,member_of WHERE Passport_no = %s and member_of.Ffp_id = ffp.Ffp_id",
            (passport,))
        ffpId = cursor_.fetchall()


        if status[0][0] == 'A':
            flash(ffpId[0][0]+' Programında SPECIAL RANK(A) sahibisiniz.')
            return redirect(url_for('index'))


if __name__ == '__main__':
    app.run(port=3000, debug=True)
