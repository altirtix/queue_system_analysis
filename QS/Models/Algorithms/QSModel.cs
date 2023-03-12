using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QS.Models.Algorithms
{
    class QSModel
    {
        public int n; // количество каналов
        public double ts; // время обслуживания
        public double lambda; // интенсивность потока заявок
        public double mu; // интенсивность потока обслуживания
        public double ro; // интенсивность нагрузки
        public double ro0; // вероятность, что канал свободен
        public double roc; // вероятность отказа (вероятность того, что канал занят)
        public double ros; // вероятность обслуживания поступающих заявок (вероятность того, что клиент будет обслужен)
        public double q; // относительная пропускная способность
        double ns; // среднее число каналов, занятых обслуживанием
        double nw; // среднее число простаивающих каналов
        double ks;  // коэффициент занятости каналов обслуживанием
        double a; // абсолютная пропускная способность
        double tw; //среднее время простоя СМО
        double roqt; //вероятность образования очереди
        double roqf; //вероятность отсутствия очереди
        double pi; // вероятность того, что придется ждать начала обслуживания
        double lq; // среднее число заявок, находящихся в очереди
        double tq; // среднее время простоя СМО
        double ls; // среднее число обслуживаемых заявок
        double lqsm; //среднее число заявок в системе
        double tqsm; //среднее время пребывания заявки в СМО
        double nominal; //номинальная производительность СМО
        double fact; // фактическая производительность СМО
        public QSModel(int nc, double ish, double ist, double ihh, double iht)
        {
            this.n = nc;
            this.ts = iht / 60;
            this.lambda = ish;
            this.mu = ist / iht;
            this.ro = this.lambda * this.ts;
        }

        static int F(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * F(x - 1);
            }
        }

        public void Calc() 
        {  
            double sum = 1;
            for (int i = 1; i <= 4; i++)
            {
                sum += (Math.Pow(this.ro, i)) / (F(i));
                if (i == 4)
                {
                    sum += (Math.Pow(this.ro, i)) / (F(i) * (i - this.ro));
                }
            }
            
            this.ro0 = (1 / sum) * 60;
            this.roc = 0;
            this.ros = 1;
            this.q = this.ros;
            this.ns = this.ro;
            this.nw = this.n - this.ns;
            this.ks = this.ns / this.n;
            this.a = this.lambda;
            this.tw = (this.roc * this.ts) * 60;
            this.roqt = ((Math.Pow(this.ro, this.n + 1)) / (F(this.n) * (this.n - this.ro))) * ro0;
            this.roqf = 1 - this.roqt;
            this.pi = (Math.Pow(this.ro, this.n)) / (F(this.n - 1) * (this.n - this.ro));
            this.lq = (this.n / (this.n - this.ro)) * roqt;
            this.tq = (this.lq / this.a) * 60;
            this.ls = this.ro;
            this.lqsm = this.lq + this.ls;
            this.tqsm = (this.lqsm / this.a) * 60;
            this.nominal = this.n / this.ts;
            this.fact = this.lambda / 60;
        }

        public override string ToString() 
        {
            if (this.ro < this.n)
            {
                return "QS IS STABLE!" + Environment.NewLine
                    + "Number of channels: " + this.n.ToString("0.000") + Environment.NewLine
                    + "Intensity of the flow of orders: " + this.lambda.ToString("0.000") + Environment.NewLine
                    + "Service flow rate: " + this.mu.ToString("0.000") + Environment.NewLine
                    + "Load Intensity: " + this.ro.ToString("0.000") + " mins" + Environment.NewLine
                    + "Probability that the channel is free: " + this.ro0.ToString("0.000") + " mins" + Environment.NewLine
                    + "Probability of failure (probability that the channel is busy): " + this.roc.ToString("0.000") + Environment.NewLine
                    + "Probability of servicing incoming requests (the probability that the client will be served): " + this.ros.ToString() + Environment.NewLine
                    + "Relative bandwidth: " + this.q.ToString("0.000") + Environment.NewLine
                    + "Average number of channels used by the service: " + this.ns.ToString("0.000") + Environment.NewLine
                    + "Average number of idle channels: " + this.nw.ToString("0.000") + Environment.NewLine
                    + "Channel busyness ratio: " + this.ks.ToString("0.000") + Environment.NewLine
                    + "Absolute Bandwidth: " + this.a.ToString("0.000") + Environment.NewLine
                    + "Average downtime of the system: " + this.tw.ToString("0.000") + " mins" + Environment.NewLine
                    + "Probability of queuing: " + this.roqt.ToString("0.000") + Environment.NewLine
                    + "Probability of no queue: " + this.roqf.ToString("0.000") + Environment.NewLine
                    + "Probability of having to wait for service to start: " + this.pi.ToString("0.000") + Environment.NewLine
                    + "Average number of orders in the queue: " + this.lq.ToString("0.000") + Environment.NewLine
                    + "Average downtime of the system: " + this.tq.ToString("0.000") + " mins" + Environment.NewLine
                    + "Average number of tickets served: " + this.ls.ToString("0.000") + Environment.NewLine
                    + "Average number of orders in the system: " + this.lqsm.ToString("0.000") + Environment.NewLine
                    + "Average time spent by a request in the QS: " + this.tqsm.ToString("0.000") + " mins" + Environment.NewLine
                    + "Nominal performance of the QS: " + this.nominal.ToString("0.000") + Environment.NewLine
                    + "Actual performance of the QS: " + this.fact.ToString("#.00") + " %" + Environment.NewLine
                    ;
            }
            else
            {
                return "QS IS UNSTABLE!";
            }

        }
    }
}
